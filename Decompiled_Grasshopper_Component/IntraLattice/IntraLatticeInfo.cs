using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace IntraLattice;

public class IntraLatticeInfo : GH_AssemblyInfo
{
	public override string Name => "IntraLattice";

	public override Bitmap Icon => null;

	public override string Description => "";

	public override Guid Id => new Guid("df475ca3-9a35-471e-9348-f2b7c04e9189");

	public override string AuthorName => "Aidan Kurtz";

	public override string AuthorContact => "aidan.kurtz@mail.mcgill.ca";
}
