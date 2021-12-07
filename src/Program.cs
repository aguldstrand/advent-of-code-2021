using System.Linq;

switch (args.FirstOrDefault())
{
    case "1.1":
        await Days.One.Parts.One.Run();
        break;

    case "1.2":
        await Days.One.Parts.Two.Run();
        break;

    case "2.1":
        await Days.Two.Parts.One.Run();
        break;

    case "2.2":
        await Days.Two.Parts.Two.Run();
        break;

    case "3.1":
        await Days.Three.Parts.One.Run();
        break;

    case "3.2":
        await Days.Three.Parts.Two.Run();
        break;

    case "4.1":
        await Days.Four.Parts.One.Run();
        break;

    case "4.2":
        await Days.Four.Parts.Two.Run();
        break;
}