using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class Champ : MonoBehaviour
{
    private String nom;
    private string imagePath;
    private string description;
    public Champ(string nom, string imagePath, string description)
    {
        this.Nom = nom;
        this.ImagePath = imagePath;
        this.Description = description;
    }
    public string Nom
    {
        get
        {
            return this.nom;
        }

        set
        {
            this.nom = value;
        }
    }

    public string ImagePath { get => imagePath; set => imagePath = value; }
    public string Description { get => description; set => description = value; }

    public override string ToString()
    {
        return $"nom : {this.Nom}" +
            $"\nImage : {this.ImagePath}" +
            $"\nDescri : {this.Description}";

    }

    public override bool Equals(object obj)
    {
        return obj is Champ chapions &&
               this.nom == chapions.nom;
    }

    public override int GetHashCode()
    {
        int hashCode = 1474526539;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nom);
        return hashCode;
    }
}
