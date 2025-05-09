using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Net.Mail;
using System.Threading.Tasks;
using Firebase.Extensions;
using System;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginpanel, signuppanel, forgotpasswordpanel, notificationpanel, environmentselectionpanel, capsule, notificationpanel2, notificationpanel3;
    public Camera UIcam;
    public InputField loginemail, loginpassword, signupemail, signuppassword, signupusername, forgotpassemail;
    public Text notif_Title_Text, notif_Message_Text, notif_Title_Text2, notif_Message_Text2, notif_Title_Text3, notif_Message_Text3;
    // public Toggle rememberMe;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    bool isSignin = false;
    public float Uicamspeed = 200f;
    private bool isXRInitialized = false;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void OpenLoginpanel()
    {
        loginpanel.SetActive(true);
        signuppanel.SetActive(false);
        forgotpasswordpanel.SetActive(false);
        environmentselectionpanel.SetActive(false);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-1091, 548, -679)) > 0.1f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-1091, 548, -679), Uicamspeed * Time.deltaTime);
        }
    }

    public void OpenSignuppanel()
    {
        loginpanel.SetActive(false);
        signuppanel.SetActive(true);
        forgotpasswordpanel.SetActive(false);
        environmentselectionpanel.SetActive(false);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-1965, 568, -679)) > 0.1f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-1965, 568, -679), Uicamspeed * Time.deltaTime);
        }
    }

    public void Openforgotpanel()
    {
        loginpanel.SetActive(false);
        signuppanel.SetActive(false);
        forgotpasswordpanel.SetActive(true);
        environmentselectionpanel.SetActive(false);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-2715, 631, -679)) > 0.0f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-2715, 631, -679), Uicamspeed * Time.deltaTime);
        }
    }

    public void Openselectenvironmentpanel()
    {
        loginpanel.SetActive(false);
        signuppanel.SetActive(false);
        forgotpasswordpanel.SetActive(false);
        environmentselectionpanel.SetActive(true);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-1749, 752, -266)) > 0.0f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-1749, 752, -266), Uicamspeed * Time.deltaTime);
        }
    }

    public void Opensafari()
    {
        loginpanel.SetActive(false);
        signuppanel.SetActive(false);
        forgotpasswordpanel.SetActive(false);
        environmentselectionpanel.SetActive(false);
        UIcam.gameObject.SetActive(false);
        InitializeXR();
        capsule.SetActive(true);
    }

    public void Loginuser()
    {
        if (string.IsNullOrEmpty(loginemail.text) && string.IsNullOrEmpty(loginpassword.text))
        {
            shownotificationmessage("Error", "Fields Empty");
            return;
        }
        SignInUser(loginemail.text, loginpassword.text);
    }

    public void Signupuser()
    {
        if (string.IsNullOrEmpty(signupemail.text) && string.IsNullOrEmpty(signuppassword.text) && string.IsNullOrEmpty(signupusername.text))
        {
            shownotificationmessage2("Error", "Fields Empty");
            return;
        }
        CreateUser(signupemail.text, signuppassword.text, signupusername.text);
    }

    public void forgotpass()
    {
        if (string.IsNullOrEmpty(forgotpassemail.text))
        {
            shownotificationmessage("Error", "Fields Empty");
            return;
        }
        forgotpasswordsubmit(forgotpassemail.text);
    }

    public void logout()
    {
        auth.SignOut();
        OpenLoginpanel();
    }

    private void shownotificationmessage(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;
        notificationpanel.SetActive(true);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-1091, 548, -679)) > 0.1f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-1091, 548, -679), Uicamspeed * Time.deltaTime);
        }
    }

    private void shownotificationmessage2(string title, string message)
    {
        notif_Title_Text2.text = "" + title;
        notif_Message_Text2.text = "" + message;
        notificationpanel2.SetActive(true);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-1965, 568, -679)) > 0.1f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-1965, 568, -679), Uicamspeed * Time.deltaTime);
        }
    }

    private void shownotificationmessage3(string title, string message)
    {
        notif_Title_Text3.text = "" + title;
        notif_Message_Text3.text = "" + message;
        forgotpasswordpanel.SetActive(false);
        notificationpanel3.SetActive(true);
        if (Vector3.Distance(UIcam.transform.position, new Vector3(-2715, 631, -679)) > 0.0f)
        {
            UIcam.transform.position = Vector3.Lerp(UIcam.transform.position, new Vector3(-2715, 631, -679), Uicamspeed * Time.deltaTime);
        }
    }

    public void close_notifpanel()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";
        notificationpanel.SetActive(false);
    }

    public void close_notifpanel2()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";
        notificationpanel2.SetActive(false);
    }

    public void close_notifpanel3()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";
        notificationpanel3.SetActive(false);
        forgotpasswordpanel.SetActive(true);
    }

    void CreateUser(string email, string password, string Username)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                Exception exception = task.Exception;
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    shownotificationmessage2("Error", GetErrorMessage(errorCode));
                }
                return;
            }
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);
            UpdateUserProfile(Username);
            Openselectenvironmentpanel();
        });
    }

    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        shownotificationmessage("Error", GetErrorMessage(errorCode));
                    }
                }
                return;
            }
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);
            Openselectenvironmentpanel();
        });
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                isSignin = true;
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void UpdateUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = UserName,
                PhotoUrl = new System.Uri("https://dummyimage.com/300"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully.");
                shownotificationmessage("Alert", "Successfully Created Account");
            });
        }
    }

    void forgotpasswordsubmit(string forgotpasswordemail)
    {
        auth.SendPasswordResetEmailAsync(forgotpasswordemail).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordEmailAsync was Canceled");
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        shownotificationmessage3("Error", GetErrorMessage(errorCode));
                    }
                }
            }
            shownotificationmessage3("Alert", "Sent Email for Password Reset");
        });
    }

    bool isSigned = false;

    void Update()
    {
        if (isSignin)
        {
            if (!isSigned)
            {
                isSigned = true;
                Openselectenvironmentpanel();
            }
        }
    }

    private static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Account does not exist";
                break;
            case AuthError.MissingPassword:
                message = "Missing Password";
                break;
            case AuthError.WeakPassword:
                message = "Password is so weak";
                break;
            case AuthError.WrongPassword:
                message = "Wrong Password";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Email already in use";
                break;
            case AuthError.InvalidEmail:
                message = "Invalid Email";
                break;
            case AuthError.MissingEmail:
                message = "Email Missing";
                break;
            default:
                message = "Invalid Error";
                break;
        }
        return message;
    }

    private void InitializeXR()
    {
        if (!isXRInitialized)
        {
            isXRInitialized = true;
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StartSubsystems();
                Debug.Log("XR initialized after environment selection.");
            }
            else
            {
                XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
                XRGeneralSettings.Instance.Manager.StartSubsystems();
                Debug.Log("XR initialized after environment selection.");
            }
        }
    }
}
