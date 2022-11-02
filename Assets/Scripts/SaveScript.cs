using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SaveScript : MonoBehaviour
{
    // define the UI objects we draw values from or update.
    public TextMeshProUGUI participantID;
    public TextMeshProUGUI gender;
    public Slider imageLikertScale;
    public RawImage tamuPresented;
    Texture tamuImage;
    string nextImage;

    void Start()
    {
        // Call RandomFilename to get a random image from the given directory to show next
        nextImage = RandomFilename("Example Images");

        // Update the picture shown in the raw image linked to this public variable to the new image selected
        tamuPresented.texture = LoadImage(nextImage);
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    // Function runs when Save Response is clicked, added to the button by adding the Game Object Save Button Manager to the OnClick()
    // element in the inspector
    public void OnButtonPress() {
        // Get the log file name for this participant
        string filename = "P" + participantID.GetParsedText();
        string emotionShown = nextImage.Substring(15);

        // Append PartID, gender, rating, the image and newline to the log file in CSV format
        File.AppendAllText("ParticipantLogs/" + filename + ".txt", // save as txt file, import to excel as CSV
                            "P"+ (participantID.GetParsedText()).ToString() + ", " + 
                            emotionShown.Remove(emotionShown.Length -4) + ", " +  //slice off directory and file extension
                            gender.GetParsedText() + ", " + 
                            imageLikertScale.value  + 
                            "\n");  

        // Call RandomFilename to get a random image from the given directory to show next
        nextImage = RandomFilename("Example Images");

        // Update the picture shown in the raw image linked to this public variable to the new image selected
        tamuPresented.texture = LoadImage(nextImage);

        // Reset the Likert Scale to neutral
        imageLikertScale.value = 4;
    }

    // load an image from filepath into a new texture variable, then return it
    private Texture2D LoadImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath))
        {
            fileData = System.IO.File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2); //..this will auto-resize the texture dimensions.
            texture.LoadImage(fileData); 
        }
        return texture;
    }

    // put all filenames in given directory into string array, then randomly select one from the array and return it
    private string RandomFilename(string dirPath)
    {
        string[] filesArray = Directory.GetFiles(dirPath);
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, filesArray.Length);
        return filesArray[randomIndex];
    }
 
}




