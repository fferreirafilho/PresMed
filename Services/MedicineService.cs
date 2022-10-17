using Microsoft.EntityFrameworkCore;
using PresMed.Data;
using PresMed.Models;
using PresMed.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Services {
    public class MedicineService : IMedicineService {

        private readonly BancoContext _context;

        public MedicineService(BancoContext context) {
            _context = context;
        }
        public async Task<List<Medicine>> FindAllAsync() {
            try {
                return await _context.Medicine.ToListAsync();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }



        public async Task InsertAsync(Medicine medicine) {
            try {
                _context.Medicine.Add(medicine);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(medicine.Name)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<Medicine> FindByIdAsync(int id) {
            try {
                return await _context.Medicine.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task UpdateAsync(Medicine medicine) {
            try {
                bool hasAny = await _context.Medicine.AnyAsync(obj => obj.Id == medicine.Id);
                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Medicine.Update(medicine);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(medicine.Name)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public Medicine TransformUpperCase(Medicine medicine) {
            medicine.Name = medicine.Name.Trim().ToUpper();
            return medicine;
        }



    }
}
