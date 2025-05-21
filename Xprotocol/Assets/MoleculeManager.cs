using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MoleculeManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Dropdown moleculeDropdown;

    [Header("Molecule Prefabs")]
    public GameObject[] moleculePrefabs;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    private GameObject currentMolecule;

    void Start()
    {
        // Clear existing options
        moleculeDropdown.ClearOptions();

        // Populate dropdown with molecule names
        foreach (GameObject prefab in moleculePrefabs)
        {
            moleculeDropdown.options.Add(new TMP_Dropdown.OptionData(prefab.name));
        }

        // Refresh the dropdown to show the new options
        moleculeDropdown.RefreshShownValue();

        // Add listener for dropdown value change
        moleculeDropdown.onValueChanged.AddListener(OnMoleculeSelected);

        // Instantiate the first molecule by default
        OnMoleculeSelected(0);
    }

    void OnMoleculeSelected(int index)
    {
        // Destroy the current molecule if it exists
        if (currentMolecule != null)
        {
            Destroy(currentMolecule);
        }

        // Instantiate the selected molecule prefab at the spawn point
        currentMolecule = Instantiate(moleculePrefabs[index], spawnPoint.position, spawnPoint.rotation);

        // Ensure the molecule has the necessary components for VR interaction
        if (!currentMolecule.GetComponent<XRGrabInteractable>())
        {
            currentMolecule.AddComponent<XRGrabInteractable>();
        }

        if (!currentMolecule.GetComponent<Rigidbody>())
        {
            Rigidbody rb = currentMolecule.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

        if (!currentMolecule.GetComponent<Collider>())
        {
            currentMolecule.AddComponent<BoxCollider>();
        }
    }
}
