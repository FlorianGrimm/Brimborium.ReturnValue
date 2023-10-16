#if false
namespace Brimborium.ReturnValue;

/*
if (NullValidator<T>.Instance.IsNull(value))
{
NullValidator
    this.Mode = OptionalMode.NoValue;
    this._Value = default;
} else
{
    this.Mode = OptionalMode.Success;
    this._Value = value;
}
*/

public class NullValidator<T>
{
    private static NullValidator<T>? _Instance;

    public static NullValidator<T> Instance
    {
        get
        {
            if (_Instance is not null) { return _Instance; }

            if (typeof(T).IsClass)
            {
                //return _Instance = new NullValidatorClass<T>();
                return _Instance = (NullValidator<T>)Activator.CreateInstance(typeof(NullValidatorClass<>).MakeGenericType(typeof(T)))!;
            } else
            {
                //return _Instance = new NullValidatorStruct<T>();
                return _Instance = (NullValidator<T>)Activator.CreateInstance(typeof(NullValidatorStruct<>).MakeGenericType(typeof(T)))!;
            }
        }
    }

    private readonly bool _IsClass;

    public NullValidator()
    {
        this._IsClass = typeof(T).IsClass;
    }

    public virtual bool IsNull([AllowNull] T value) => this._IsClass ? this.IsNullClass(value) : this.IsNullStruct(value);


    private bool IsNullClass([AllowNull] T value) => ReferenceEquals(null, value);

    private bool IsNullStruct([AllowNull] T value) => false;
}
public class NullValidatorClass<T> : NullValidator<T>
    where T : class
{
    public override bool IsNull([AllowNull] T value) => ReferenceEquals(null, value);
}

public class NullValidatorStruct<T> : NullValidator<T>
    where T : struct
{
    public override bool IsNull([AllowNull] T value) => false;
}
#endif