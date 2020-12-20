namespace FEM
{
    public static class UniversalElementCalculator
    {
        public static double N1Function(double ksi, double eta)
        {
            return 0.25 * (1 - ksi) * (1 - eta);
        }

        public static double N2Function(double ksi, double eta)
        {
            return 0.25 * (1 + ksi) * (1 - eta);
        }

        public static double N3Function(double ksi, double eta)
        {
            return 0.25 * (1 + ksi) * (1 + eta);
        }

        public static double N4Function(double ksi, double eta)
        {
            return 0.25 * (1 - ksi) * (1 + eta);
        }

        public static double DN1DKsiFunction(double eta)
        {
            return -0.25 * (1 - eta);
        }

        public static double DN2DKsiFunction(double eta)
        {
            return 0.25 * (1 - eta);
        }

        public static double DN3DKsiFunction(double eta)
        {
            return 0.25 * (1 + eta);
        }

        public static double DN4DKsiFunction(double eta)
        {
            return -0.25 * (1 + eta);
        }

        public static double DN1DEtaFunction(double ksi)
        {
            return -0.25 * (1 - ksi);
        }

        public static double DN2DEtaFunction(double ksi)
        {
            return -0.25 * (1 + ksi);
        }

        public static double DN3DEtaFunction(double ksi)
        {
            return 0.25 * (1 + ksi);
        }

        public static double DN4DEtaFunction(double ksi)
        {
            return 0.25 * (1 - ksi);
        }
    }
}