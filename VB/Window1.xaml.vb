Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Printing

' ...
Namespace UseCollectionViewLink

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Create a link and bind it to the PrintPreview instance.
            Dim link As CollectionViewLink = New CollectionViewLink()
            'preview.Model = new LinkPreviewModel(link);
            ' Create an ICollectionView object.
            link.CollectionView = CreateMonthCollectionView()
            ' Provide export templates.
            link.DetailTemplate = CType(Resources("monthNameTemplate"), DataTemplate)
            link.GroupInfos.Add(New GroupInfo(CType(Resources("monthQuarterTemplate"), DataTemplate)))
            ' Create a document.
            link.CreateDocument(True)
            ' Show a Print Preview.
            PrintHelper.ShowPrintPreviewDialog(Me, link)
        End Sub

        Private Function CreateMonthCollectionView() As ICollectionView
            Const monthCount As Integer = 12
            Dim monthNames As String() = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
            Dim data As MonthItem() = New MonthItem(Math.Min(monthNames.Length, monthCount) - 1) {}
            For i As Integer = 0 To data.Length - 1
                data(i) = New MonthItem(monthNames(i), i \ 3 + 1)
            Next

            Dim source As CollectionViewSource = New CollectionViewSource()
            source.Source = data
            source.GroupDescriptions.Add(New PropertyGroupDescription("Quarter"))
            Return source.View
        End Function

        Public Class MonthItem

            Private _Name As String, _Quarter As Integer

            Public Property Name As String
                Get
                    Return _Name
                End Get

                Private Set(ByVal value As String)
                    _Name = value
                End Set
            End Property

            Public Property Quarter As Integer
                Get
                    Return _Quarter
                End Get

                Private Set(ByVal value As Integer)
                    _Quarter = value
                End Set
            End Property

            Public Sub New(ByVal name As String, ByVal quarter As Integer)
                Me.Name = name
                Me.Quarter = quarter
            End Sub
        End Class
    End Class
End Namespace
