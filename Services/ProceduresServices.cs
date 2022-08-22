using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using PresMed.Models.Enums;

namespace PresMed.Services {
    public class ProceduresServices : IProceduresServices {
        private readonly BancoContext _context;

        public ProceduresServices(BancoContext context) {
            _context = context;
        }
        public async Task<List<Procedures>> FindAllActiveAsync() {
            try {
                var list = await _context.Procedure.ToListAsync();
                return list.Where(x => x.Status == Status.Ativo).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Procedures>> FindAllDisableAsync() {
            try {
                var list = await _context.Procedure.ToListAsync();
                return list.Where(x => x.Status == Status.Desativado).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }


        public async Task InsertAsync(Procedures procedure) {
            try {
                _context.Procedure.Add(procedure);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(procedure.Name)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<Procedures> FindByIdAsync(int id) {
            try {
                return await _context.Procedure.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }
        public async Task UpdateAsync(Procedures procedure) {
            try {
                bool hasAny = await _context.Procedure.AnyAsync(obj => obj.Id == procedure.Id);
                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Procedure.Update(procedure);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public Procedures TransformUpperCase(Procedures procedure) {
            procedure.Name = procedure.Name.Trim().ToUpper();
            return procedure;
        }


    }
}
