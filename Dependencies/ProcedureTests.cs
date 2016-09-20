using Should;
using Dependencies;

public class ProcedureTests
{
    // The following tests were created against a known stored procedure
    // with known stored procedure dependencies.
    readonly StoredProcedure SP;

    public ProcedureTests()
    {
        string procedureName = "dbo.StoredProcedureName";
        SP = new StoredProcedure(procedureName);
        SP.LookForDependencies();
    }

    public void ShouldNotBeNull()
    {
        SP.Name.ShouldNotBeNull();
    }

    public void ShouldHaveProcedureDependencies()
    {
        SP.Procedures.Count.ShouldBeGreaterThan(0);
    }

    public void ShouldHaveThreeProcedureDependencies()
    {
        SP.Procedures.Count.ShouldEqual(3);
    }

    public void DependencyShouldHaveEightProcedureDependencies()
    {
        SP.Procedures[1].Procedures.Count.ShouldEqual(8);
    }

    public void DependencyDepthShouldBeThree()
    {
        SP.DependencyDepth.ShouldEqual(3);
    }

    public void NumberOfProcedureCallsShouldBeFourteen()
    {
        SP.NumberOfCalls.ShouldEqual(14);
    }

    public void StoredProcedureHasTables()
    {
        SP.Procedures[1].Dependencies.Count.ShouldBeGreaterThan(0);
    }

}
