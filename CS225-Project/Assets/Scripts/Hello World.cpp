#include <iostream>
#include <fstream>

using namespace std;

int main() {
    ofstream outFile;
    ifstream inFile;
    
    try {
        outFile.open("Hello_World.txt");

        if (!outFile.is_open()) {
            throw string("Failed to write to file.");
        }

        outFile << "Hello World!";
        outFile.close();

        inFile.open("Hello_World.txt");

        if (!inFile.is_open()) {
            throw string("Failed to read from file.");
        }
        
        string readFromFile;
        getline(inFile, readFromFile);
        cout << readFromFile << endl;
        
        inFile.close();
    }
    
    // catch for failed file openning
    catch (string fileError) {
        cout << "ERROR: " << fileError << endl;
        return 1;
    }

    return 0;
}