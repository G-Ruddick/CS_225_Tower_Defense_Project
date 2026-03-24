using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Heightandwidth : MonoBehaviour {
    public class mapScaleValue {
        private int size;
        
        public mapScaleValue(int x = 7) {size = x;}
        public int getSize() {return size;}

        // overloading the ++ and -- for the buttons
        public static mapScaleValue operator++ (mapScaleValue MSV) {
            MSV.size++;
            return MSV;
        }
        public static mapScaleValue operator-- (mapScaleValue MSV) {
            MSV.size--;
            return MSV;
        }
    }
    
    public Button Height_UP;
    public Button Height_Down;
    public Button Width_UP;
    public Button Width_Down;

    public TMP_Text Height_Text; 
    public TMP_Text Width_Text;

    public mapScaleValue Height;
    public mapScaleValue Width;

    void Start() {
        Height = new mapScaleValue();
        Width = new mapScaleValue();

        // checking for button presses
        Height_UP.onClick.AddListener(() => buttonClickIncrease(Height));
        Height_Down.onClick.AddListener(() => buttonClickDecrease(Height));
        Width_UP.onClick.AddListener(() => buttonClickIncrease(Width));
        Width_Down.onClick.AddListener(() => buttonClickDecrease(Width));
    }

    void Update() {
        // preventing a map size from being too small
        if (Height.getSize() < 8 || Width.getSize() > Height.getSize() * 1.5 ) {
            Height_Down.interactable = false;
        }
        else {
            Height_Down.interactable = true;
        }
        if (Width.getSize() < 8 || Width.getSize() == Height.getSize()) {
            Width_Down.interactable = false;
        }
        else {
            Width_Down.interactable = true;
        }

        // preventing height from being greater than the width or too large
        if (Height.getSize() >= Width.getSize() || Height.getSize() > 30) {
            Height_UP.interactable = false;
        }
        else {
            Height_UP.interactable = true;
        }
        // preventing width from being too much larger than height or too large
        if (Width.getSize() > Height.getSize() * 1.5 || Width.getSize() > 30) {
            Width_UP.interactable = false;
        }
        else {
            Width_UP.interactable = true;
        }

        // updating the text
        Height_Text.text = "" + Height.getSize();
        Width_Text.text = "" + Width.getSize();
    }
    
    void buttonClickIncrease(mapScaleValue MSV) {
        MSV++;
    }
    void buttonClickDecrease(mapScaleValue MSV) {
        MSV--;
    }
}
