using System;
using UnityEngine;

[Serializable]
public class EvolutionStage {
    public string 외형;
    public string 이름;
    public string 한줄특징;
    public string Hint;
}

[Serializable]
public class DepartmentEvolution {
    public string 외관;
    public EvolutionStage _1단계;
    public EvolutionStage _2단계;
    public EvolutionStage _3단계;
}

[Serializable]
public class EvolutionDatabaseWrapper {
    public DepartmentEvolution ECC;
    public DepartmentEvolution 포관;
    public DepartmentEvolution 공대;
}
