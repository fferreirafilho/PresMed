using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ISchedulingServices {

        public Task<List<Scheduling>> FindByIdAndDateAsync(int DoctorId, DateTime DayAttendence);
        public Task InsertAsync(Scheduling scheduling);

        public Task<List<Scheduling>> FindBylargerDate(int DoctorId, DateTime DayAttendence);

        public Task<Scheduling> FindByIdAsync(int id);
        public Task UpdateAsync(Scheduling scheduling);
        public Task Delete(Scheduling scheduling);
    }
}
