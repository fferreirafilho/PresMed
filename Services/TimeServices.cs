using Microsoft.EntityFrameworkCore;
using PresMed.Data;
using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PresMed.Models.Enums;

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

        public async Task<List<Time>> FindAllActiveAsync() {
            return await _context.Time.Include(obj => obj.Person).Where(obj => obj.Person.Status == Status.Ativo).ToListAsync();

        }

        public async Task<Time> FindByIdAsync(int id) {

            return await _context.Time.Include(Obj => Obj.Person).FirstOrDefaultAsync(obj => obj.Id == id);
        }
    }
}
