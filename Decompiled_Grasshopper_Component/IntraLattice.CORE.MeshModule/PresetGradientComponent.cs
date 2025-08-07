using System;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Helpers;
using IntraLattice.Properties;

namespace IntraLattice.CORE.MeshModule;

public class PresetGradientComponent : GH_Component
{
	private GH_Document GrasshopperDocument;

	private IGH_Component Component;

	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.presetGradient;

	public override Guid ComponentGuid => new Guid("{6a4e5dcf-5d72-49fc-a543-c2465b14eb86}");

	public PresetGradientComponent()
		: base("Preset Gradient", "PresetGradient", "Generates gradient string (i.e. a spatial math expression)", "IntraLattice", "Utils")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddIntegerParameter("Gradient Type", "Type", "Selection of gradient types", (GH_ParamAccess)0, 0);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddTextParameter("Gradient String", "Grad", "The spatial gradient as an expression string", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		Component = (IGH_Component)(object)this;
		GrasshopperDocument = ((GH_DocumentObject)this).OnPingDocument();
		if (Component.Params.Input[0].SourceCount == 0)
		{
			InputTools.GradientSelect(ref Component, ref GrasshopperDocument, 0, 11f);
		}
		int num = 0;
		if (DA.GetData<int>(0, ref num))
		{
			string text = null;
			switch (num)
			{
			case 0:
				text = "Abs(x)";
				break;
			case 1:
				text = "Abs(y)";
				break;
			case 2:
				text = "Abs(z)";
				break;
			case 3:
				text = "Abs(2*x-1)";
				break;
			case 4:
				text = "Abs(2*y-1)";
				break;
			case 5:
				text = "Abs(2*z-1)";
				break;
			case 6:
				text = "Sqrt(Abs(2*y-1)^2 + Abs(2*z-1)^2)/Sqrt(2)";
				break;
			case 7:
				text = "Sqrt(Abs(2*x-1)^2 + Abs(2*z-1)^2)/Sqrt(2)";
				break;
			case 8:
				text = "Sqrt(Abs(2*x-1)^2 + Abs(2*y-1)^2)/Sqrt(2)";
				break;
			case 9:
				text = "Sqrt(Abs(2*x-1)^2 + Abs(2*y-1)^2 + Abs(2*z-1)^2)/Sqrt(3)";
				break;
			}
			DA.SetData(0, (object)text);
		}
	}
}
