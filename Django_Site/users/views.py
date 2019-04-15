# users/views.py
from django.urls import reverse_lazy
from django.views import generic

from .forms import CustomUserCreationForm, StudentSignUpForm, TeacherSignUpForm
from .models import CustomUser, Teacher, Student

from django.contrib.auth import login
from django.shortcuts import redirect, get_object_or_404, render

from django.http import HttpResponse, HttpResponseRedirect

from .forms import EditStudent

class SignUp(generic.CreateView):
    form_class = CustomUserCreationForm
    success_url = reverse_lazy('login')
    template_name = 'signup.html'

class StudentSignUpView(generic.CreateView):
    model = CustomUser
    form_class = StudentSignUpForm
    template_name = 'registration/signup_form.html'

    def get_context_data(self, **kwargs):
        kwargs['user_type'] = 'student'
        return super().get_context_data(**kwargs)

    def form_valid(self, form):
        user = form.save()
        login(self.request, user)
        return redirect('home')


class TeacherSignUpView(generic.CreateView):
    model = CustomUser
    form_class = TeacherSignUpForm
    template_name = 'registration/signup_form.html'

    def get_context_data(self, **kwargs):
        kwargs['user_type'] = 'teacher'
        return super().get_context_data(**kwargs)

    def form_valid(self, form):
        user = form.save()
        login(self.request, user)
        return redirect('home')

# @login_required
# @stud]

def teacherHome(request):
    teacher = request.user
    return render(request, 'teacherHome.html', {'teacher': teacher})

def viewClass(request):
    user = request.user
    teacher = user.teacher
    student_list = Student.objects.all()
    return render(request, 'viewClass.html', {'student_list': student_list, 't': teacher})

def studentReport(request, student_id):
    user = get_object_or_404(CustomUser, id = student_id)
    student = user.student
    teacher = request.user.teacher
    return render(request, 'studentReport.html', {'student': student, 'teacher': teacher})
    
def addStudents(request):
    user = request.user
    teacher = user.teacher
    student_list = Student.objects.all()
    return render(request, 'addStudents.html', {'student_list': student_list, 't': teacher})

def added(request, student_id):
    user = get_object_or_404(CustomUser, id = student_id)
    student = user.student
    teacher = request.user.teacher
    student.teacher_id = teacher
    student.save()
    return render(request, 'added.html', {'student': student, 'teacher': teacher})


def removed(request, student_id):
    user = get_object_or_404(CustomUser, id = student_id)
    student = user.student
    teacher = request.user.teacher
    student.teacher_id = None
    student.save()
    return render(request, 'removed.html', {'student': student, 'teacher': teacher})

def editStudent(request, student_id):
    teacher = request.user.teacher
    user = get_object_or_404(CustomUser, id = student_id)
    student = user.student

    if request.method == 'POST':
        form = EditStudent(request.POST)
        if form.is_valid():
            # student.total_xp = form.cleaned_data['experience_value']
            print(student.total_xp)
            change = form.cleaned_data['experience_value']
            print(change)
            student.total_xp = change
            student.save()
            return redirect('viewClass')

    else:
        form = EditStudent()
    
    return render(request, 'editStudent.html', {'student': student, 'teacher': teacher, 'form':form})


