# users/models.py
from django.contrib.auth.models import AbstractUser
from django.db import models

class CustomUser(AbstractUser):
    # add additional fields in here
    id = models.IntegerField(auto_created=True, primary_key=True)
    fname = models.CharField(default='', max_length=15)
    lname = models.CharField(default='', max_length=25)
    birthdate = models.DateField(default='1900-01-01')
    is_student = models.BooleanField('student status', default=False)
    is_teacher = models.BooleanField('teacher status', default=False)
    def __str__(self):
        string = ' '
        string = self.fname + string + self.lname
        return string

class Teacher(models.Model):
    user = models.OneToOneField(CustomUser, on_delete=models.CASCADE, primary_key=True)
    # def __str__(self):
    #     return self.user.__str__

class Student(models.Model):
    user = models.OneToOneField(CustomUser, on_delete=models.CASCADE, primary_key=True)
    teacher_id = models.ForeignKey(Teacher, on_delete=models.SET_NULL, null=True, default=None)
    total_xp = models.IntegerField(default=0)
    # def __str__(self):
    #     return self.user.__str__

class GameInstance(models.Model):
    id = models.IntegerField(auto_created=True, primary_key=True)
    user_id = models.ForeignKey(Student, on_delete=models.CASCADE)
    points_scored = models.IntegerField()
    game_type = models.IntegerField()
    # points_possible = 100
    def __str__(self):
        return id.__str__
