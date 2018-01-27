public static class RoverControllerHelper
{
    private static System.Type[] cachedInstructionTypes;

    public static System.Type[] GetInstructionTypes()
    {
        if (RoverControllerHelper.cachedInstructionTypes == null)
        {
            System.Collections.Generic.List<System.Type> instructionTypes = new System.Collections.Generic.List<System.Type>();

            System.Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            for (int index = 0; index < types.Length; ++index)
            {
                System.Type type = types[index];
                if (type.IsAbstract || !type.IsSubclassOf(typeof(RoverInstruction)))
                {
                    continue;
                }

                instructionTypes.Add(type);
            }

            RoverControllerHelper.cachedInstructionTypes = instructionTypes.ToArray();
        }

        return RoverControllerHelper.cachedInstructionTypes;
    }
}