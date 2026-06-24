using UnityEngine;
using UnityEngine.InputSystem;

public class SceneBootstrapper : MonoBehaviour
{
    public enum ModoEscena { Escena1, Escena2, Escena3 }
    public ModoEscena modoDeEscenaActual;
    public FormationManager formationManager;
    public Blackboard blackboard;
    public float umbralProximidad;
    private LinePattern patronLinea;
    private TrianglePattern patronTriangulo;
    private CirclePattern patronCirculo;

    private void Start()
    {
        patronLinea = new LinePattern();
        patronTriangulo = new TrianglePattern();
        patronCirculo = new CirclePattern();

        formationManager.SetPattern(patronLinea);

        blackboard.expertos.Add(new FormationExpert());

        if (modoDeEscenaActual == ModoEscena.Escena2 || modoDeEscenaActual == ModoEscena.Escena3)
        {
            blackboard.expertos.Add(new DistractionExpert());
        }
    }

    private void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame || Keyboard.current[Key.Numpad1].wasPressedThisFrame)
        {
            formationManager.SetPattern(patronLinea);
        }
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame || Keyboard.current[Key.Numpad2].wasPressedThisFrame)
        {
            formationManager.SetPattern(patronTriangulo);
        }
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame || Keyboard.current[Key.Numpad3].wasPressedThisFrame)
        {
            formationManager.SetPattern(patronCirculo);
        }

        if (modoDeEscenaActual == ModoEscena.Escena2 || modoDeEscenaActual == ModoEscena.Escena3)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 posicionRaton = Mouse.current.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(posicionRaton);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 puntoClicado = hit.point;
                    puntoClicado.y = formationManager.transform.position.y;
                    bool algunAgenteCerca = false;
                    for (int i = 0; i < formationManager.slots.Count; i++)
                    {
                        float distancia = Vector3.Distance(formationManager.slots[i].agente.transform.position, puntoClicado);
                        if (distancia <= umbralProximidad)
                        {
                            algunAgenteCerca = true;
                            break;
                        }
                    }

                    if (algunAgenteCerca)
                    {
                        if (formationManager.anchorPoint != null)
                        {
                            formationManager.anchorPoint.position = puntoClicado;
                        }
                        else
                        {
                            formationManager.transform.position = puntoClicado;
                        }

                        blackboard.SetData("punto_distraccion", typeof(Vector3), puntoClicado);
                    }
                    else
                    {
                        Debug.Log("Fuera del alcance de los agentes!");
                    }
                }
            }
        }
        blackboard.UpdateBlackboard();
    }
}