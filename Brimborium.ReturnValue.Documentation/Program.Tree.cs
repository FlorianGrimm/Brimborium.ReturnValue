namespace Brimborium.ReturnValue;

using Brimborium.ReturnValue;

internal partial class Program
{
    static void MainTree()
    {
        var root = new Tree(null, "root");
        var a = new Tree(root, string.Empty);

        #region assign Name but not Parent

        a = a with
        {
            Name = "a"
        };

        // - or -

        a = a.WithNullable(null, "a");

        #endregion assign Name but not Parent

        System.Diagnostics.Trace.Assert("root" == a.Parent?.Name);
        System.Diagnostics.Trace.Assert("a" == a.Name);

        var a2 = a.WithOptional(NoValue.Value, "aa");
        System.Diagnostics.Trace.Assert("aa" == a2.Name);

        Tree? tree = null;
        var a3 = a.WithOptional(tree.AsOptional(), "aa");
        System.Diagnostics.Trace.Assert("aa" == a3.Name);

        var a4 = a.WithOptional((Tree?)null, "aa");
        System.Diagnostics.Trace.Assert("aa" == a4.Name);

    }
}

#region sample Tree
public record class Tree(
    Tree? Parent,
    string Name)
{
    public Tree WithNullable(Tree? parent, string? name)
    {
        return new Tree(parent ?? this.Parent, name ?? this.Name);
    }

    public Tree WithOptional(Brimborium.ReturnValue.Optional<Tree?> parent, Optional<string> name)
    {
        return new Tree(parent.GetValueOrDefault(this.Parent), name.GetValueOrDefault(this.Name));
    }
}
#endregion sample Tree