using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

[ToolboxData("<{0}:fiUpload runat=server></{0}:fiUpload>")]
public partial class fiUpload : UserControl
{
    // File Upload Control - fiUpload
    // Developed by Selçuk ERMAYA
    // My Web Site : www.selcukermaya.com
    // v1.1

    [Bindable(true)]
    [Localizable(true)]

    [Category("Caption Properties")]
    public string UploadCaption
    {
        get
        {
            String s = (String)ViewState["Text"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["Text"] = value;
            lblCaption.Text = value;
        }
    }
    [Category("Caption Properties")]
    public string UploadButtonCaption
    {
        get
        {
            String s = (String)ViewState["Text"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["Text"] = value;
            btnUpload.Text = value;
        }
    }
    [Category("Caption Properties")]
    public string DeleteButtonCaption
    {
        get
        {
            String s = (String)ViewState["Text"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["Text"] = value;
            btnDelete.Text = value;
        }
    }
    [Category("Caption Properties")]
    public string ComeBackButtonCaption
    {
        get
        {
            String s = (String)ViewState["Text"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["Text"] = value;
            btnComeBack.Text = value;
        }
    }
    [Category("Upload Messages")]
    public string DeleteFileError
    {
        get
        {
            String s = (String)ViewState["DeleteFileError"];
            return ((s == null) ? Language.Admin["DeleteFileError"] : s);
        }

        set
        {
            ViewState["DeleteFileError"] = value;
        }
    }
    [Category("Upload Messages")]
    public string DeletedFile
    {
        get
        {
            String s = (String)ViewState["DeletedFile"];
            return ((s == null) ? Language.Admin["FileDeleted"] : s);
        }

        set
        {
            ViewState["DeletedFile"] = value;
        }
    }
    [Category("Upload Messages")]
    public string FileExistError
    {
        get
        {
            String s = (String)ViewState["FileExistError"];
            return ((s == null) ? Language.Admin["ExistError"] : s);
        }

        set
        {
            ViewState["FileExistError"] = value;
        }
    }
    [Category("Upload Messages")]
    public string FolderPathError
    {
        get
        {
            String s = (String)ViewState["FolderPathError"];
            return ((s == null) ? Language.Admin["FolderPathError"] : s);
        }

        set
        {
            ViewState["FolderPathError"] = value;
        }
    }
    [Category("Upload Messages")]
    public string FileSelectError
    {
        get
        {
            String s = (String)ViewState["FileSelectError"];
            return ((s == null) ? Language.Admin["FileSelectError"] : s);
        }

        set
        {
            ViewState["FileSelectError"] = value;
        }
    }
    [Category("Upload Messages")]
    public string ExtensionError
    {
        get
        {
            String s = (String)ViewState["ExtensionError"];
            return ((s == null) ? Language.Admin["ExtensionError"].Replace("%", Extensions) : s);
        }

        set
        {
            ViewState["ExtensionError"] = value;
        }
    }
    [Category("Upload Messages")]
    public string UploadCompleate
    {
        get
        {
            String s = (String)ViewState["UploadCompleate"];
            return ((s == null) ? Language.Admin["UploadCompleate"] : s);
        }

        set
        {
            ViewState["UploadCompleate"] = value;
        }
    }
    [Category("Upload Messages")]
    public string FileSizeError
    {
        get
        {
            String s = (String)ViewState["FileSizeError"];
            return ((s == null) ? Language.Admin["FileSizeError"].Replace("%", FileSize + " KB") : s);
        }

        set
        {
            ViewState["FileSizeError"] = value;
        }
    }
    [Bindable(true, BindingDirection.TwoWay), Category("Upload Properties")]
    public string FileName
    {
        get
        {
            String s = (String)ViewState["FileName"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["FileName"] = value;
            lblFile.Text = value;
        }
    }
    [Category("Upload Properties")]
    public string FileSize
    {
        get
        {
            String s = (String)ViewState["FileSize"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["FileSize"] = value;
        }
    }
    [Category("Upload Properties")]
    public bool OverwriteFile
    {
        get
        {
            bool s = false;
            if (ViewState["OverWriteFile"] != null)
            {
                s = (bool)ViewState["OverwriteFile"];
            }
            return ((s == null) ? false : s);
        }

        set
        {
            ViewState["OverwriteFile"] = value;
        }
    }
    [Category("Upload Properties")]
    public bool ShowPictures
    {
        get
        {
            bool s = false;
            if (ViewState["ShowPictures"] != null)
            {
                s = (bool)ViewState["ShowPictures"];
            }
            return ((s == null) ? false : s);
        }

        set
        {
            ViewState["ShowPictures"] = value;
        }
    }
    [Category("Upload Properties")]
    public string UploadFolder
    {
        get
        {
            String s = (String)ViewState["UploadFolder"];
            return ((s == null) ? "/" : s);
        }

        set
        {
            ViewState["UploadFolder"] = value;
        }
    }
    [Category("Upload Properties")]
    public string Extensions
    {
        get
        {
            String s = (String)ViewState["Extensions"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["Extensions"] = value;
        }
    }
    [Category("DataBinding")]
    public string DataBindLabelControlID
    {
        get
        {
            String s = (String)ViewState["DataBindLabelControlID"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["DataBindLabelControlID"] = value;
        }
    }
    [Category("DataBinding")]
    public string ControlError
    {
        get
        {
            String s = (String)ViewState["ControlError"];
            return ((s == null) ? "Databind Control Not Found !" : s);
        }

        set
        {
            ViewState["ControlError"] = value;
        }
    }
    [Category("DataBinding")]
    public bool EditMode
    {
        get
        {
            bool s = false;
            if (ViewState["EditMode"] != null)
            {
                s = (bool)ViewState["EditMode"];
            }
            return ((s == null) ? false : s);
        }

        set
        {
            ViewState["EditMode"] = value;
        }
    }
    [Category("Upload Properties")]
    public string ImageExtensions
    {
        get
        {
            String s = (String)ViewState["ImageExtensions"];
            return ((s == null) ? String.Empty : s);
        }

        set
        {
            ViewState["ImageExtensions"] = value;
        }
    }
    public static string SunucudakiDosya;
    protected void Page_Load(object sender, EventArgs e)
    {
        divCaption.Visible = false;
        divControls.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (EditMode == true)
        {
            if (DataBindLabelControlID != null)
            {
                string File = (this.Parent.FindControl(DataBindLabelControlID) as Label).Text;
                if (File != null & File != "")
                {
                    FileName = (this.Parent.FindControl(DataBindLabelControlID) as Label).Text;
                    ShowControls(true);
                }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fupFile.HasFile != true)
        {
            lblErrorMessage.Text = FileSelectError;
        }
        else
        {
            if (Extensions == "" || UzantiKontrol(Path.GetExtension(fupFile.PostedFile.FileName.ToString()), Extensions))
            {
                bool FolderOk = Directory.Exists(Server.MapPath(UploadFolder));
                if (!FolderOk)
                {
                    Directory.CreateDirectory(Server.MapPath(UploadFolder));
                }
                FolderOk = Directory.Exists(Server.MapPath(UploadFolder));
                if (FolderOk)
                {
                    if (!File.Exists(Server.MapPath(UploadFolder + fupFile.FileName)))
                    {
                        Uploaded();
                    }
                    else
                    {
                        if (OverwriteFile)
                        {
                            Uploaded();
                        }
                        else
                        {
                            lblErrorMessage.Text = FileExistError;
                            SunucudakiDosya = fupFile.FileName;
                            btnComeBack.Visible = true;
                        }
                    }

                }
                else
                {
                    lblErrorMessage.Text = FolderPathError;
                }

            }
            else
            {
                ShowControls(false);
                lblErrorMessage.Text = ExtensionError.Replace("%", Extensions);
            }
        }
    }
    public bool UzantiKontrol(string Uzanti, string Uzantilar)
    {
        string[] Ext = Uzantilar.Split(',');
        bool Uzan = false;
        for (int i = 0; i < Ext.Length; i++)
        {
            if ("." + Ext[i] == Uzanti.ToLower())
            {
                Uzan = true;
                break;
            }
        }
        return Uzan;
    }
    public void ShowControls(bool Compleate)
    {
        if (Compleate)
        {
            lblCaption.Visible = false;
            fupFile.Visible = false;
            btnUpload.Visible = false;
            btnComeBack.Visible = false;
            btnDelete.Visible = true;
            lblFile.Visible = true;
        }
        else
        {
            lblCaption.Visible = true;
            fupFile.Visible = true;
            btnUpload.Visible = true;
            btnComeBack.Visible = false;
            btnDelete.Visible = false;
            lblFile.Visible = false;
            imgPicture.Visible = false;
            lblFile.Text = "";
            FileName = "";
            lblErrorMessage.Text = "";
            if (DataBindLabelControlID != null & DataBindLabelControlID != "")
            {
                try
                {
                    (this.Parent.FindControl(DataBindLabelControlID) as Label).Text = FileName;
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = ControlError + " : " + ex.Message;
                }
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            File.Delete(Server.MapPath(UploadFolder + FileName));
            ShowControls(false);
            lblErrorMessage.Text = DeletedFile;
            if (DataBindLabelControlID != null & DataBindLabelControlID != "")
            {
                try
                {
                    (this.Parent.FindControl(DataBindLabelControlID) as Label).Text = FileName;
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = ControlError + " : " + ex.Message;
                }
            }
            FileName = "";
            SunucudakiDosya = "";
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = DeleteFileError + " : " + ex.Message;
        }

    }
    protected void btnComeBack_Click(object sender, EventArgs e)
    {
        ShowControls(true);
        FileName = SunucudakiDosya;
        lblErrorMessage.Text = UploadCompleate;
        if (DataBindLabelControlID != null & DataBindLabelControlID != "")
        {
            try
            {
                (this.Parent.FindControl(DataBindLabelControlID) as Label).Text = FileName;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ControlError + " : " + ex.Message;
            }
        }

    }
    public void Uploaded()
    {
        try
        {
            if (FileSize == null || FileSize == "")
            {
                FileSize = "9999999";
            }
            if (FileSize == null || Convert.ToInt32(fupFile.FileBytes.Length / 1024) < Convert.ToInt32(FileSize))
            {
                fupFile.SaveAs(Server.MapPath(UploadFolder + fupFile.FileName));
                ShowControls(true);
                lblErrorMessage.Text = UploadCompleate;
                FileName = fupFile.FileName;
            }
            else
            {
                lblErrorMessage.Text = FileSizeError.Replace("%", FileSize);
            }
        }
        catch (System.Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }

    public void NewFile()
    {
        ShowControls(false);
        FileName = "";
    }
}


