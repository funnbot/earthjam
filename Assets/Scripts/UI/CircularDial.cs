using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class CircularDial : Selectable, IPointerClickHandler
{
    [SerializeField] private RectTransform m_NeedleRect;
    public RectTransform needleRect { get => m_NeedleRect; set { if (SetPropertyUtility.SetClass(ref m_NeedleRect, value)) { UpdateVisuals(); } } }

    [SerializeField, Range(0f, 1f)] private float m_Value;
    public float value { get => m_Value; set { Set(value); } }

    [SerializeField] private float m_MinAngle = 0f;
    public float minAngle { get => m_MinAngle; set { if (SetPropertyUtility.SetStruct(ref m_MinAngle, value)) { ValidateAngleRange(); UpdateVisuals(); }; } }

    [SerializeField] private float m_MaxAngle = 360f;
    public float maxAngle { get => m_MaxAngle; set { if (SetPropertyUtility.SetStruct(ref m_MaxAngle, value)) { ValidateAngleRange(); UpdateVisuals(); }; } }

    [SerializeField] private bool m_Delimited;
    public bool delimited { get => m_Delimited; set { if (SetPropertyUtility.SetStruct(ref m_Delimited, value)) { Set(m_Value); }; } }

    [SerializeField, Range(1, 100)] private int m_Steps;
    public int steps { get => m_Steps; set { if (SetPropertyUtility.SetStruct(ref m_Steps, value)) { m_Steps = Mathf.Clamp(m_Steps, 1, 100); Set(m_Value); }; } }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }

    public virtual void Set(float newValue)
    {
        newValue = Mathf.Clamp(newValue, 0f, 1f);

        if (m_Delimited && newValue != 0f)
        {
            float oneOverSteps = 1 / ((float)m_Steps);
            newValue = Mathf.Round(newValue / oneOverSteps) * oneOverSteps;
        }
        if (newValue == m_Value)
            return;
        m_Value = newValue;
        UpdateVisuals();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateAngleRange();
        Set(m_Value);
    }

    void OnDrawGizmosSelected()
    {
        Quaternion cwMaxRot = Quaternion.Euler(0f, 0f, 360f - m_MinAngle);
        Vector3 minOffset = cwMaxRot * (Vector3.up * 10f);
        Quaternion cwMinRot = Quaternion.Euler(0f, 0f, 360f - m_MaxAngle);
        Vector3 maxOffset = cwMinRot * (Vector3.up * 10f);

        Gizmos.DrawRay(transform.position, minOffset);
        Gizmos.DrawRay(transform.position, maxOffset);
    }

    private void ValidateAngleRange()
    {
        m_MinAngle = Mathf.Clamp(m_MinAngle, -360f, m_MaxAngle);
        m_MaxAngle = Mathf.Clamp(m_MaxAngle, m_MinAngle, 360f);
    }

    private void UpdateVisuals()
    {
        float cwMaxAngle = 360f - m_MinAngle;
        float cwMinAngle = 360f - m_MaxAngle;
        float cwValue = 1f - m_Value;

        float diff = Mathf.Abs(cwMinAngle - cwMaxAngle);
        // remap [0, 1] to [minAngle, maxAngle]
        float remapped = cwMinAngle + cwValue * diff;
        m_NeedleRect.localRotation = Quaternion.Euler(0f, 0f, remapped);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
