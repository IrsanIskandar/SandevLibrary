using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Reporting;

namespace SandevLibraryClassicNetFramework.Reporting
{
    public partial class DoctorPrescription : Report
    {
        public DoctorPrescription()
        {
            InitializeComponent();
        }

        public DoctorPrescription(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
