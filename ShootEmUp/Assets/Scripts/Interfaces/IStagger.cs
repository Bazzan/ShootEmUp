using UnityEngine;


public enum StaggerType {Stagger, Pushback};

public interface IStagger
{

    void Stagger(StaggerType staggerType, Vector3 force);




}
