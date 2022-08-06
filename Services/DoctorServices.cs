using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using PresMed.Models.Enums;

namespace PresMed.Services {
    public class DoctorServices : IDoctorServices {
        private readonly BancoContext _context;

        public DoctorServices(BancoContext context) {
            _context = context;
        }

        public async Task InsertAsync(Doctor doctor) {
            try {
                _context.Doctor.Add(doctor);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(doctor.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar esse usuario, CPF duplicado");
                }
                if (e.InnerException.Message.Contains(doctor.User)) {
                    throw new Exception($"Houve um erro ao salvar esse usuario, usuario duplicado");
                }
                if (e.InnerException.Message.Contains(doctor.Crm)) {
                    throw new Exception($"Houve um erro ao salvar esse usuario, CRM duplicado");
                }
                throw new Exception($"Houve um erro ao salvar o usuario erro: {e.InnerException.Message}");

            }
        }

        public async Task<List<Doctor>> FindAllDisableAsync() {

            try {
                var list = await _context.Doctor.ToListAsync();
                return list.Where(x => x.Status == UserStatus.Inativado).ToList();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar os usuarios tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task<List<Doctor>> FindAllActiveAsync() {

            try {
                var list = await _context.Doctor.ToListAsync();
                return list.Where(x => x.Status == UserStatus.Ativo).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar os usuarios tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task<Doctor> FindByIdAsync(int id) {

            try {
                return await _context.Doctor.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar o usuario tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task UpdateAsync(Doctor doctor) {

            try {
                bool hasAny = await _context.Doctor.AnyAsync(x => x.Id == doctor.Id);

                if (!hasAny) {
                    throw new Exception("Id not found");
                }
                try {
                    _context.Doctor.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e) {
                    throw new Exception(e.Message);
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }


        }
    }
}
