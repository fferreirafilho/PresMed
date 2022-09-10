using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using PresMed.Models.Enums;

namespace PresMed.Services {
    public class PatientServices : IPatientServices {
        private readonly BancoContext _context;

        public PatientServices(BancoContext context) {
            _context = context;
        }

        public async Task InsertAsync(Person patient) {
            try {

                _context.Person.Add(patient);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(patient.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(patient.User)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(patient.Crm)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");

            }
        }

        public async Task<List<Person>> FindAllDisableAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == Status.Desativado && x.PersonType == PersonType.Patient).ToList();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Person>> FindAllActiveAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == Status.Ativo && x.PersonType == PersonType.Patient).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Person>> FindAllAsync() {

            try {
                return await _context.Person.OrderBy(x => x.Name).ToListAsync();
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

        public async Task UpdateAsync(Person patient) {

            try {
                bool hasAny = await _context.Person.AnyAsync(x => x.Id == patient.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Person.Update(patient);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }


        }

        public Person TransformUpperCase(Person patient) {

            patient.Name = patient.Name.Trim().ToUpper();
            patient.Email = patient.Email.Trim().ToUpper();
            patient.Cpf = patient.Cpf.Trim().ToUpper();
            patient.Street = patient.Street.Trim().ToUpper();
            patient.District = patient.District.Trim().ToUpper();
            patient.State = patient.State.Trim().ToUpper();
            patient.City = patient.City.Trim().ToUpper();
            patient.User = patient.User.Trim().ToUpper();

            if (patient.Number != null && patient.Number != "") {
                patient.Number = patient.Number.Trim().ToUpper();
            }


            if (patient.Complement != null && patient.Complement != "") {
                patient.Complement = patient.Complement.Trim().ToUpper();
            }

            return patient;
        }
    }
}
