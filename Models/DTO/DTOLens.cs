using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticaMultivisual.Models.DTO
{
    internal class DTOLens : dbContext
    {
        private int lens_ID;
        private string OD_esfera;
        private double OD_cilindro;
        private double OD_eje;
        private int OD_prisma;
        private int OD_adicion;
        private string OI_esfera;
        private double OI_cilindro;
        private double OI_eje;
        private int OI_prisma;
        private int OI_adicion;

        public int lens_ID1 { get => lens_ID; set => lens_ID = value; }
        public string OD_esfera1 { get => OD_esfera; set => OD_esfera = value; }
        public double OD_cilindro1 { get => OD_cilindro; set => OD_cilindro = value; }
        public double OD_eje1 { get => OD_eje; set => OD_eje = value; }
        public int OD_prisma1 { get => OD_prisma; set => OD_prisma = value; }
        public int OD_adicion1 { get => OD_adicion; set => OD_adicion = value; }
        public string OI_esfera1 { get => OI_esfera; set => OI_esfera = value; }
        public double OI_cilindro1 { get => OI_cilindro; set => OI_cilindro = value; }
        public double OI_eje1 { get => OI_eje; set => OI_eje = value; }
        public int OI_prisma1 { get => OI_prisma; set => OI_prisma = value; }
        public int OI_adicion1 { get => OI_adicion; set => OI_adicion = value; }
    }
}
