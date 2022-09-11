using Microsoft.EntityFrameworkCore;
using System.Linq;
using PresMed.Data;
using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public class SchedulingServices : ISchedulingServices {

        private readonly BancoContext _context;

        public SchedulingServices(BancoContext context) {
            _context = context;
        }

        public async Task<List<Scheduling>> FindByIdAndDateAsync(int DoctorId, DateTime DayAttendence) {
            try {
                return await _context.Scheduling.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Procedures).Where(x => x.Doctor.Id == DoctorId && x.DayAttendence == DayAttendence).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar, Erro: {ex.Message}");
            }
        }
        public async Task InsertAsync(Scheduling scheduling) {
            try {
                _context.Scheduling.Add(scheduling);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<List<Scheduling>> FindBylargerDate(int DoctorId, DateTime DayAttendence) {
            try {
                return await _context.Scheduling.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Procedures).Where(x => x.Doctor.Id == DoctorId && x.DayAttendence >= DayAttendence).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar, Erro: {ex.Message}");
            }
        }
        public async Task<Scheduling> FindByIdAsync(int id) {
            try {
                return await _context.Scheduling.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Procedures).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar, Erro: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Scheduling scheduling) {
            try {
                _context.Scheduling.Update(scheduling);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar, Erro: {ex.Message}");
            }
        }
        public async Task Delete(Scheduling scheduling) {
            try {
                _context.Scheduling.Remove(scheduling);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar, Erro: {ex.Message}");
            }
        }
    }
}
