using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookUpTable<T1, T2>
{
    //Creamos un delegate que tome T1 como parametro y devuelva T2
    //public delegate T2 Ecuacion(T1 keyToReturn);
    //Ecuacion _factoryMethod;
    
    Func<T1, T2> _ecuacionFunc;

    //Creamos un diccionario donde guardamos la Key T1 y el valor resultante T2
    Dictionary<T1, T2> _table;

    //Constructor en donde vamos a recibir la funcion con la ecuacion y ya inicializamos el dictionary
    public LookUpTable(Func<T1, T2> newFactory)
    {
        _ecuacionFunc = newFactory;

        _table = new Dictionary<T1, T2>();
    }

    public T2 ReturnValue(T1 myKey)
    {
        if (_table.ContainsKey(myKey))
        {
            Debug.Log($"Devuelvo el valor de {myKey}");
            return _table[myKey];
        }
        else
        {
            Debug.Log($"Genero el valor de {myKey}");

            //Lo generamos
            T2 value = _ecuacionFunc(myKey);

            //Almacenamos
            _table[myKey] = value;

            //Devolvemos
            return value;
        }
    }
}