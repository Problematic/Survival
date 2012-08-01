using UnityEngine;
using System.Collections;

public interface IFightable {
	Status GetStatus();
}

public class Genericenemy : IFightable {
	private Status status;
	public Status GetStatus() {return status;}
	public Genericenemy() {
		status = new Status(50, 10, 10, 10);
	}
}