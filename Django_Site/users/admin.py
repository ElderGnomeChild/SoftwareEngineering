# users/admin.py
from django.contrib import admin
from django.contrib.auth import get_user_model
from django.contrib.auth.admin import UserAdmin, User

from .forms import CustomUserCreationForm, CustomUserChangeForm
from .models import CustomUser

class CustomUserAdmin(UserAdmin):
    add_form = CustomUserCreationForm
    form = CustomUserChangeForm
    model = CustomUser
    list_display = ['id', 'username', 'fname', 'lname', 'is_teacher']

# class Student(User):
#     add_form = StudentCreationForm
#     form = StudentChangeForm
#     model = Student
#     list_display = ['lname', 'fname', 'birthdate']

admin.site.register(CustomUser, CustomUserAdmin)