using System.Collections;
using UnityEngine;

public class LocationHolder : MonoBehaviour
{
    [SerializeField] private Location[] locations;
    public int LocationAmount => locations.Length - 1;
    public Location this[int _index]
    {
        get
        {
            if (_index > locations.Length - 1) return null;
            print(locations[_index].gameObject.name);
            return locations[_index];
        }
    }
    private void Awake()
    {
        foreach(var _location in locations)
        {
            DisactivateLocation(_location);
        }
        ActivateLocation(locations[0]);
    }
    public void ActivateLocation(Location _location)
    {
        SetStateLocation(_location, true);
    }
    public void DisactivateLocation(Location _location)
    {
        SetStateLocation(_location, false);
    }
    private void SetStateLocation(Location _location, bool _state)
    {
        _location.gameObject.SetActive(_state);
    }
    public void RegenerateLocation(Location _location)
    {
        _location.RegenerateLocation();
    }
}