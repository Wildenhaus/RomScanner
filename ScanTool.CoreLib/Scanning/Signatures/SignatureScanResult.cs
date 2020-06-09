using System;
using System.Diagnostics.CodeAnalysis;

namespace ScanTool.CoreLib.Scanning.Signatures
{

  public readonly struct SignatureScanResult : IEquatable<SignatureScanResult>
  {

    public readonly string Name;
    public readonly long Address;

    public SignatureScanResult( string name, long address )
    {
      Name = name;
      Address = address;
    }

    public bool Equals( [AllowNull] SignatureScanResult other )
      => Address == other.Address
      && Name == other.Name;

    public override int GetHashCode()
      => Address.GetHashCode();

  }

}
