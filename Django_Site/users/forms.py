from django import forms
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
from .models import CustomUser, Student, Teacher
from django.db import transaction

class CustomUserCreationForm(UserCreationForm):

    class Meta(UserCreationForm):
        model = CustomUser
        fields = ('username', 'fname', 'lname', 'birthdate')

class CustomUserChangeForm(UserChangeForm):

    class Meta:
        model = CustomUser
        fields = ('username', 'fname', 'lname', 'birthdate')


class StudentSignUpForm(UserCreationForm):

    class Meta(UserCreationForm.Meta):
        model = CustomUser
        fields = ('fname', 'lname', 'birthdate')

    @transaction.atomic
    def save(self):
        user = super().save(commit=False)
        user.username = (user.fname + user.lname).lower()
        user.is_student = True
        # high = max(Student.objects.get(all).id)
        high = 0

        for STU in CustomUser.objects.all():
            if STU.id >= high:
                high = STU.id + 1

        user.id = high
        user.save()
        # print(user)
        student = Student.objects.create(user=user)
        return user

class TeacherSignUpForm(UserCreationForm):
    class Meta(UserCreationForm.Meta):
        model = CustomUser
        fields = ('username', 'email', 'fname', 'lname')

    @transaction.atomic
    def save(self, commit=True):
        user = super().save(commit=False)
        user.is_teacher = True


        high = 0

        for TEA in CustomUser.objects.all():
            if TEA.id >= high:
                high = TEA.id + 1

        user.id = high
        user.save()
        teacher = Teacher.objects.create(user=user)
        
        return user

class EditStudent(forms.Form):
    # student_id = forms.IntegerField()
    experience_value = forms.IntegerField()

    # user = CustomUser.objects.get()
    # user.student.total_xp = experience_value
