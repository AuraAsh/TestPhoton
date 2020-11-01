using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Objectspool: MonoBehaviour
{
    [System.Serializable] // Public class aparece en el inspector
    public class Pool
    {
        public string tag; // Nombre prefab inspector
        public GameObject prefab;
        public int size; // Tamaño de grupo de almacenamiento, numero de activos a la vez (Reutilizar objetos en lugar de usar nuevos)
    }

    #region Singleton // Unica instancia accesible, clase de tipo administrador global

    public static Objectspool Instance;

    private void Awake() //Configuracion de instance
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools; // Lista publica inspector
    public Dictionary<string, Queue<GameObject>> poolDictionary; // Etiqueta para asociar con cada pool, lo que se quiere almacenar en el Queue

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); // Nuevo diccionario para comenzar a llenarlo en el grupo de objetos

        foreach (Pool pool in pools) //foreach para llamar a cada pool en la lista
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); // Crea Queue o nueva cola de objetos

            for (int i = 0; i < pool.size; i++) // Se agrega todos los objetos "pool" en Queue
            {
                GameObject obj = Instantiate(pool.prefab); // Se llena todo el pool o grupo de objetos, creando instancias a todos los objetos
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    //Reutiliza elementos en la cola o Queue
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // si un elemento o tag no esta dentro de un pool, este no genera errores
        {
            print("Get SpawnFromPool");
            return null; // Devolver un objeto instanciado que se crea
        }

        // objeto generado, posicion para mover donde se quiera
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPoolObject poolObject = objectToSpawn.GetComponent<IPoolObject>(); // Responde al metodo de interface, obtiene un componente del objeto de instancia, asignado a una variable

        if (poolObject != null) //Objeto que hereda de la Interface
        {
            poolObject.OnObjectSpawn(); //Llama funcion en esta interfaz 
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        print("Exit SpawnFromPool");
        return objectToSpawn; // Obtener el objeto desde donde se genero 

    }
}
