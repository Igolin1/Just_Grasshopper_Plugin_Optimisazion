using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace IntraLattice.Properties;

[DebuggerNonUserCode]
[CompilerGenerated]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("IntraLattice.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap adjustUV
	{
		get
		{
			object @object = ResourceManager.GetObject("adjustUV", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap basicBox
	{
		get
		{
			object @object = ResourceManager.GetObject("basicBox", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap basicCylinder
	{
		get
		{
			object @object = ResourceManager.GetObject("basicCylinder", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap cleanNetwork
	{
		get
		{
			object @object = ResourceManager.GetObject("cleanNetwork", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap conformSA
	{
		get
		{
			object @object = ResourceManager.GetObject("conformSA", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap conformSP
	{
		get
		{
			object @object = ResourceManager.GetObject("conformSP", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap conformSS
	{
		get
		{
			object @object = ResourceManager.GetObject("conformSS", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap customCell
	{
		get
		{
			object @object = ResourceManager.GetObject("customCell", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap heterogenCustom
	{
		get
		{
			object @object = ResourceManager.GetObject("heterogenCustom", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap heterogenGradient
	{
		get
		{
			object @object = ResourceManager.GetObject("heterogenGradient", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap homogen
	{
		get
		{
			object @object = ResourceManager.GetObject("homogen", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap meshPreview
	{
		get
		{
			object @object = ResourceManager.GetObject("meshPreview", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap meshReport
	{
		get
		{
			object @object = ResourceManager.GetObject("meshReport", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap presetCell
	{
		get
		{
			object @object = ResourceManager.GetObject("presetCell", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap presetGradient
	{
		get
		{
			object @object = ResourceManager.GetObject("presetGradient", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap uniformDS
	{
		get
		{
			object @object = ResourceManager.GetObject("uniformDS", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal Resources()
	{
	}
}
