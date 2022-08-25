using Microsoft.EntityFrameworkCore;
using PresMed.Data;
using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Services {
    public class TimeServices : ITimeServices {

        protected readonly BancoContext _context;

        public TimeServices(BancoContext bancoContext) {
            _context = bancoContext;
        }

        public async Task InsertAsync(Time time) {
            try {
                await _context.AddAsync(time);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(Time time) {
            try {
                _context.Update(time);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Time>> FindAllAsync() {
            try {
                var list = await _context.Time.ToListAsync();
                return list;
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }
    }
}
