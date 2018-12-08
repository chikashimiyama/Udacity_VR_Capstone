using System;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace DomainF
{
	public interface ITutorialBehaviour
	{
		void SetText(string text);
		void Open();
		void Close();
	}

	public class TutorialBehaviour : MonoBehaviour, ITutorialBehaviour
	{
		[SerializeField] private GameObject tutorialPanel;
		[SerializeField] private Text textField;
		[SerializeField] private Button nextButton;
		[SerializeField] private Button SkipButton;
		
		public void SetText(string text)
		{
			textField.text = text;
		}

		public void Open()
		{
			tutorialPanel.SetActive(true);
		}

		public void Close()
		{
			tutorialPanel.SetActive(false);	
		}
		
		public void OnNextButtonClicked()
		{
			
		}

		public void OnSkipButtonClicked()
		{
			
		}
	}

#pragma warning restore CS0649

 }



