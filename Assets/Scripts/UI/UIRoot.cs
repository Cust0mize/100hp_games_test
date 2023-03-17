using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIRoot : MonoBehaviour
{
    private readonly static Dictionary<string, AbstractPanel> _panels = new Dictionary<string, AbstractPanel>();
    public string ActivePanelName { get; private set; }

    private AbstractPanel _currentPanel;

    private void Start() {
        RefreshPanelsDatabase();
    }

    private void RefreshPanelsDatabase() {
        _panels.Clear();

        for (var i = 0; i < transform.childCount; i++) {
            var panel = transform.GetChild(i).GetComponent<AbstractPanel>();
            if (panel != null) {
                _panels.Add(panel.name, panel);
            }
        }
    }

    public void HideAllPanels() {
        foreach (var kvp in _panels) {
            if (kvp.Value.gameObject.activeSelf)
                kvp.Value.Hide(null);
        }
    }

    public T GetPanel<T>() where T : AbstractPanel {
        var pName = typeof(T).Name;

        if (!_panels.ContainsKey(pName)) throw new System.Exception("Panel not found with name " + pName);

        return _panels[pName] as T;
    }

    public T GetPanel<T>(string name) where T : AbstractPanel {
        if (!_panels.ContainsKey(name)) throw new System.Exception("Panel not found with name " + name);

        return _panels[name] as T;
    }

    public void ShowPanel<T>(System.Action onComplete = null) {
        ShowPanel(typeof(T).Name, onComplete);
    }

    public void ShowPanel(string name, System.Action onComplete = null) {
        if (!_panels.ContainsKey(name)) throw new System.Exception("Panel not found with name " + name);

        _panels[name].transform.SetAsLastSibling();
        _panels[name].Show(() =>
        {
            ActivePanelName = name;
            onComplete?.Invoke();
        });
    }

    public void HidePanel<T>(System.Action onComplete = null) {
        HidePanel(typeof(T).Name, onComplete);
    }

    public void HidePanel(string name, System.Action onComplete = null) {
        if (!_panels.ContainsKey(name)) throw new System.Exception("Panel not found with name " + name);
        _panels[name].Hide(() =>
        {
            ActivePanelName = "";

            onComplete += OnComplitedActive;
            onComplete?.Invoke();
            onComplete -= OnComplitedActive;
        });
    }

    private void OnComplitedActive() {
        // find any other panel left active
        foreach (var kvp in _panels) {
            if (kvp.Value.gameObject.activeSelf) {
                ActivePanelName = kvp.Key;
                break;
            }
        }
    }

    public void SwapToPanel<T>(bool isInstant = true, System.Action onComplete = null) {
        SwapToPanel(typeof(T).Name, isInstant, onComplete);
    }

    public void SwapToPanel(string toPanel, bool isInstant = true, System.Action onComplete = null) {
        if (!_panels.ContainsKey(toPanel)) throw new System.Exception("Panel not found with name " + toPanel);

        if (ActivePanelName == toPanel) return;

        if (string.IsNullOrEmpty(ActivePanelName)) {
            _panels[toPanel].transform.SetAsLastSibling();
            _panels[toPanel].Show(() =>
            {
                ActivePanelName = toPanel;
                onComplete?.Invoke();
            });
        }
        else {
            _panels[toPanel].transform.SetAsLastSibling();
            _panels[toPanel].Show(() =>
            {
                _panels[ActivePanelName].Hide(() =>
                {
                    ActivePanelName = toPanel;
                    onComplete?.Invoke();
                });
            });
        }
    }
}
