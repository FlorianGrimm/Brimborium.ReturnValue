# Brimborium.ReturnValue

Simple Optional Result

If Exception is not the right answer.

## Null is not evial

IMHO: Null is an important value like zero for math.

Zero has important role in math. The fact that you cannot divide by zero, makes zero not bad and divide not evial.

The fact that you cannot dereference null, makes null not bad and dereferencung nor evial.

# Optional

IMHO: The JavaScript undefined is obvious for not found - and simpler compared to an out parameter e.g. Dictionary<,>.TryGetValue(out).

So my challange is this:

given this:

[sample Tree](./Brimborium.ReturnValue.Documentation/Program.Tree.cs)

```C#
public record class Tree(
	Tree? Parent,
	string Name)
{
	public Tree WithNullable(Tree? parent, string? name) { 
		return new Tree(parent??this.Parent, name??this.Name);
	}

	public Tree WithOptional(Brimborium.ReturnValue.Optional<Tree?> parent, Optional<string> name)
	{
		return new Tree(parent.GetValueOrDefault(this.Parent), name.GetValueOrDefault(this.Name));
	}
}
```

You can update the Name:

[assign Name but not Parent](./Brimborium.ReturnValue.Documentation/Program.Tree.cs)
```C#
		a = a with
		{
			Name = "a"
		};

		// - or -

		a = a.WithNullable(null, "a");
```

but you cannot set the Parent to null.