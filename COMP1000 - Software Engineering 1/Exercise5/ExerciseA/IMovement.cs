using System;

public interface IMovement
{
	/// <summary>
	/// defines the relationship between an integer number and a variable name
	/// allows you to use meaningful representations of values for character movement
	/// maps numbers 1 to 4 to names
	/// </summary>
	public enum Compass {North, South, East, West};

	/// <summary>
	/// Converts a character for movement into a numeric value derived from the IMovement.Compass enum
	/// </summary>
	/// <param name="direction">a single character representing a key direction from wasd and ijkl</param>
	/// <returns>returns to numeric representation of the direction from Compass</returns>
	public int Move(char direction);

	/// <summary>
	/// Converts an integer into the correct Compass enum value
	/// </summary>
	/// <param name="direction">the int value which needs to be converted into a MovmenentDirection </param>
	/// <returns>the affiliated Compass value that equals the given int direction</returns>
	public Compass Move(int direction);

	/// <summary>
	/// Converts an Compass direction into the correct integer value
	/// </summary>
	/// <param name="direction">the int value which needs to be converted into a MovmenentDirection </param>
	/// <returns>the affiliated Compass value that equals the given int direction</returns>
	public int Move(Compass direction);

	/// <summary>
	/// From an array of strings, find the one that is set to a value and convert it to its upper case version and return only the string
	/// </summary>
	/// <param name="message">an array containing a mixed case message</param>
	/// <returns>an upper case version of the supplied sub-message.</returns>
	public string CapitaliseText(string [] message);

}
