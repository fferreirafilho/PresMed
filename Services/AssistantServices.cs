using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using PresMed.Models.Enums;

namespace PresMed.Services {
    public class AssistantServices : IAssistantServices {
        private readonly BancoContext _context;

        public AssistantServices(BancoContext context) {
            _context = context;
        }

        public async Task InsertAsync(Person assistant) {
            try {
                assistant.SetPasswordHash();
                _context.Person.Add(assistant);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(assistant.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(assistant.User)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                if (e.InnerException.Message.Contains(assistant.Crm)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar o usuario, erro: {e.InnerException.Message}");

            }
        }

        public async Task<List<Person>> FindAllDisableAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == Status.Desativado && x.PersonType == PersonType.Assistant).ToList();

            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Person>> FindAllActiveAsync() {

            try {
                var list = await _context.Person.ToListAsync();
                return list.Where(x => x.Status == Status.Ativo && x.PersonType == PersonType.Assistant).ToList();
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

        public async Task UpdateAsync(Person assistant) {

            try {
                bool hasAny = await _context.Person.AnyAsync(x => x.Id == assistant.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }

                _context.Person.Update(assistant);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public Person TransformUpperCase(Person assistant) {

            assistant.Name = assistant.Name.Trim().ToUpper();
            assistant.Email = assistant.Email.Trim().ToUpper();
            assistant.Cpf = assistant.Cpf.Trim().ToUpper();
            assistant.Street = assistant.Street.Trim().ToUpper();
            assistant.District = assistant.District.Trim().ToUpper();
            assistant.State = assistant.State.Trim().ToUpper();
            assistant.City = assistant.City.Trim().ToUpper();
            assistant.User = assistant.User.Trim().ToUpper();

            if (assistant.Number != null && assistant.Number != "") {
                assistant.Number = assistant.Number.Trim().ToUpper();
            }


            if (assistant.Complement != null && assistant.Complement != "") {
                assistant.Complement = assistant.Complement.Trim().ToUpper();
            }

            return assistant;
        }
    }

}
