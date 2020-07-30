using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public class Testing : MonoBehaviour
{

    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    // Start is called before the first frame update
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds),
            typeof(MoveSpeedComponent)
        );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(10000, Allocator.Temp);

        entityManager.CreateEntity(entityArchetype, entityArray);

        for(int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelComponent { level = UnityEngine.Random.Range(10, 100) });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { moveSpeed = UnityEngine.Random.Range(1f, 2f) });
            entityManager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-8, 8f), UnityEngine.Random.Range(-8, 8f), 0f) });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = this.mesh,
                material = this.material,
            });
        }
        entityArray.Dispose();
    }

}
