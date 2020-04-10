using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AreaBehaviour : RootBehaviour
{

    [NotNull]
    public GameObject vertexPrefab;
    public GameObject connectionPrefab;
    public Text sceneText;

    private GameObject lastConnection;

    private IShape currentShape;

    private List<Type> knownTypes;

    private GameObjectPool vertexPool;
    private GameObjectPool connectionPool;

    public List<GameObject> connections = new List<GameObject>();
    private List<GameObject> vertexes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        vertexPool = new GameObjectPool(vertexPrefab);
        connectionPool = new GameObjectPool(connectionPrefab);

        knownTypes = new List<Type>();
        foreach (Type type in
            Assembly.GetAssembly(typeof(Shape)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Shape))))
        {
            knownTypes.Add(type);
        }


        Action();
    }

    public void Action()
    {
        lastConnection = null;

        for (int i=0; i<vertexes.Count; i++)
        {
            vertexPool.InsertObject(vertexes[i]);
        }
        vertexes.Clear();

        for (int i = 0; i < connections.Count; i++)
        {
            connectionPool.InsertObject(connections[i]);
        }
        connections.Clear();

        currentShape = new Shape();
        AddVertex(new Vector3(3, -2, 0));
        AddVertex(new Vector3(3, 2, 0));
        AddVertex(new Vector3(-3, 2, 0));
        AddVertex(new Vector3(-3, -2, 0));
        EndFigure();
    }

    public void AddVertex(Vector3 position)
    {
        GameObject vertex = vertexPool.GetObject();
        vertex.transform.position = position;
        vertexes.Add(vertex);

        if (lastConnection != null)
        {
            GameObject connection = connectionPool.GetObject();
            connection.GetComponent<ConnectionBehaviour>().CreateConnection(lastConnection.transform.position, vertex.transform.position);
            connections.Add(connection);
        }
        lastConnection = vertex;
        currentShape.AddPoint(position);
    }

    private void EndFigure()
    {
        Vector3 firstPos = currentShape.GetPositions()[0];
        if (lastConnection != null)
        {
            GameObject connection = connectionPool.GetObject();
            connection.GetComponent<ConnectionBehaviour>().CreateConnection(lastConnection.transform.position, firstPos);
            connections.Add(connection);
        }

        foreach (Type t in knownTypes)
        {
            IShape sub_validator = (IShape)Activator.CreateInstance(t);
            sub_validator.SetPositions(currentShape.GetPositions());
            if (sub_validator.IsShape())
            {
                sceneText.text = "Shape is " + t.Name;
                return; ;
            }
        }
        sceneText.text = "Not known shape!";
    }
}
