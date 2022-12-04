using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using PresMed.Models.Enums;

namespace PresMed.Services {
    public class CidServices : ICidServices {
        private readonly BancoContext _context;

        public CidServices(BancoContext context) {
            _context = context;
        }
        public async Task<List<Cid>> FindAllAsync() {
            try {
                return await _context.Cid.OrderBy(x => x.Cod).ToListAsync();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }


        public async Task InsertAsync(Cid cid) {
            try {
                _context.Cid.Add(cid);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(cid.Cod)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<Cid> FindByIdAsync(int id) {
            try {
                return await _context.Cid.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task UpdateAsync(Cid cid) {
            try {
                bool hasAny = await _context.Cid.AnyAsync(obj => obj.Id == cid.Id);
                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Cid.Update(cid);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public Cid TransformUpperCase(Cid cid) {
            cid.Cod = cid.Cod.Trim().ToUpper();
            cid.Description = cid.Description.Trim().ToUpper();
            return cid;
        }


    }
}
