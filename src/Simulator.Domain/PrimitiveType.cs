namespace Simulator.Domain;

/// <summary>
/// Represents a primitive data type in the system.
/// </summary>
public enum PrimitiveType
{
    /// <summary>
    /// Boolean type (true or false).
    /// </summary>
    Bool,

    /// <summary>
    /// 8-bit unsigned integer.
    /// </summary>
    Byte,

    /// <summary>
    /// 8-bit signed integer.
    /// </summary>
    Sbyte,

    /// <summary>
    /// Unicode 16-bit character.
    /// </summary>
    Char,

    /// <summary>
    /// 128-bit high-precision decimal.
    /// </summary>
    Decimal,

    /// <summary>
    /// 64-bit floating-point number.
    /// </summary>
    Double,

    /// <summary>
    /// 32-bit floating-point number.
    /// </summary>
    Float,

    /// <summary>
    /// 32-bit signed integer.
    /// </summary>
    Int,

    /// <summary>
    /// 32-bit unsigned integer.
    /// </summary>
    Uint,

    /// <summary>
    /// Native-size signed integer.
    /// </summary>
    Nint,

    /// <summary>
    /// Native-size unsigned integer.
    /// </summary>
    Nuint,

    /// <summary>
    /// 64-bit signed integer.
    /// </summary>
    Long,

    /// <summary>
    /// 64-bit unsigned integer.
    /// </summary>
    Ulong,

    /// <summary>
    /// 16-bit signed integer.
    /// </summary>
    Short,

    /// <summary>
    /// 16-bit unsigned integer.
    /// </summary>
    Ushort
}
