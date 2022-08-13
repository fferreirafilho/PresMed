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
                    throw new Exception($"Houve um erro ao salvar esse usuario, CPF duplicado");
                }
                if (e.InnerException.Message.Contains(patient.User)) {
                    throw new Exception($"Houve um erro ao salvar esse usuario, usuário duplicado");
                }
                if (e.InnerException.Message.Contains(patient.Crm)) {
                    throw new Exception($"Houve um erro ao salvar esse usuario, CRM duplicado");
                }
                throw new Exception($"Houve um erro ao salvar o usuario, erro: {e.InnerException.Message}");

            }
        }

        public async Task<List<Person>> FindAllDisableAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == UserStatus.Inativado && x.PersonType == PersonType.Patient).ToList();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar os usuarios tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task<List<Person>> FindAllActiveAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == UserStatus.Ativo && x.PersonType == PersonType.Patient).ToList();
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar os usuarios tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task<Person> FindByIdAsync(int id) {

            try {
                return await _context.Person.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar o usuario tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task UpdateAsync(Person patient) {

            try {
                bool hasAny = await _context.Person.AnyAsync(x => x.Id == patient.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                try {
                    _context.Person.Update(patient);
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
