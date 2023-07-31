using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class HealthBar : MonoBehaviour {
      [SerializeField] private Slider _slider;
      //[SerializeField] private Damageable _damageable;
     [SerializeField] private Enemy _enemy;

      private void OnValidate() {
        //_slider = GetComponent<Slider>();
        //_enemy = GetComponent<Enemy>();
        _slider.maxValue = _enemy.Health.Value;
      }

      public void SetHealthBar(float damage) {
        _slider.value -= damage;
      }
    }
}
