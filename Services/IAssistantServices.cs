﻿using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IAssistantServices {
        public Task InsertAsync(Person assistant);

        public Task<List<Person>> FindAllActiveAsync();
        public Task<List<Person>> FindAllDisableAsync();

        public Task<Person> FindByIdAsync(int id);

        public Task UpdateAsync(Person assistant);

        public Person TransformUpperCase(Person assistant);
    }
}
