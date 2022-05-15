using UnityEngine;

public class BaseObstacles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particlesOnCollision;

    public void PlayParticle()
    {
        foreach(var _particle in particlesOnCollision)
        {
            _particle.Play();
        }
    }
}