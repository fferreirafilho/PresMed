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

        public async Task InsertAsync(Person doctor) {
            try {
                doctor.SetPasswordHash();
                _context.Person.Add(doctor);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(doctor.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(doctor.User)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(doctor.Crm)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<List<Person>> FindAllDisableAsync() {

            try {
                var list = await _context.Person.OrderBy(p => p.Name).ToListAsync();
                return list.Where(x => x.Status == Status.Desativado && x.PersonType == PersonType.Doctor).ToList();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Person>> FindAllActiveAsync() {

            try {
                var list = await _context.Person.OrderBy(p => p.Name).ToListAsync();
                return list.Where(x => x.Status == Status.Ativo && x.PersonType == PersonType.Doctor).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<Person> FindByIdAsync(int id) {

            try {
                return await _context.Person.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task UpdateAsync(Person doctor) {

            try {
                bool hasAny = await _context.Person.AnyAsync(x => x.Id == doctor.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Person.Update(doctor);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }


        }

        public Person TransformUpperCase(Person doctor) {

            doctor.Name = doctor.Name.Trim().ToUpper();
            doctor.Email = doctor.Email.Trim().ToUpper();
            doctor.Cpf = doctor.Cpf.Trim().ToUpper();
            doctor.Street = doctor.Street.Trim().ToUpper();
            doctor.District = doctor.District.Trim().ToUpper();
            doctor.State = doctor.State.Trim().ToUpper();
            doctor.City = doctor.City.Trim().ToUpper();
            doctor.User = doctor.User.Trim().ToUpper();
            doctor.Crm = doctor.Crm.Trim().ToUpper();
            doctor.Speciality = doctor.Speciality.Trim().ToUpper();

            if (doctor.Number != null && doctor.Number != "") {
                doctor.Number = doctor.Number.Trim().ToUpper();
            }


            if (doctor.Complement != null && doctor.Complement != "") {
                doctor.Complement = doctor.Complement.Trim().ToUpper();
            }

            return doctor;
        }
    }

}
