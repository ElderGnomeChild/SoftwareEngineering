from django.urls import path
from . import views

urlpatterns = [
    path('signup/', views.SignUp.as_view(), name='signup'),
    path('signup/student/', views.StudentSignUpView.as_view(), name='student_signup'),
    path('signup/teacher/', views.TeacherSignUpView.as_view(), name='teacher_signup'),
    path('viewClass', views.viewClass, name='viewClass'),
    path('teacherHome', views.teacherHome, name='teacherHome'),
    path('studentReport/<int:student_id>', views.studentReport, name='studentReport'),
    path('addStudents', views.addStudents, name='addStudents'),
    path('added/<int:student_id>', views.added, name='added'),
    path('removed/<int:student_id>', views.removed, name='removed')
]