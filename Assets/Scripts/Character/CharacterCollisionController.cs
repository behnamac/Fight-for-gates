using System;
using System.Collections;
using System.Collections.Generic;
using Character.Gaint;
using Collecteble;
using UnityEngine;

namespace Character
{
    public class CharacterCollisionController : MonoBehaviour
    {
        public GaintController gaint;
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectebleCollision collecteble))
            {
                collecteble.OnCollide(this);
            }
        }
    }
}
