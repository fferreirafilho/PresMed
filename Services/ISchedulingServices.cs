using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ISchedulingServices {

        public Task<List<Scheduling>> FindByIdAsync(int DoctorId, DateTime DayAttendence);
        public Task InsertAsync(Scheduling scheduling);
    }
}
