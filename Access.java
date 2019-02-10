import java.sql.*;

public class Access {
    // private Connection conn;
    private static final String DB_URL = "jdbc:sqlite:328project.db";
    private String s = " ";


    public Access() {
    };

    private String dateBuilder(int year, int month, int day) {
        // if (month < 10){ 
        //     String m = "0" + "1"
        // }
        String date = year + "-" + month + "-" + day;
        return date;
    }

    public void addStudent(String fname, String lname, int year, int month, int day, int teacher_id) {

        String birthdate = dateBuilder(year, month, day);

        try (Connection conn = DriverManager.getConnection(DB_URL)) {
            PreparedStatement statement = conn.prepareStatement("insert into Student (fname, lname, birthdate, teacher_id, total_xp) VALUES (?, ?, ?, ?, 0)");

            
            statement.setString(1, fname);
            statement.setString(2, lname);
            statement.setString(3, birthdate);
            statement.setInt(4, teacher_id);
            statement.execute();

        } catch (SQLException ex) {
            ex.printStackTrace();
        }
    };

    public int updateXP(int student_id) {
        int total = 0;
        try (Connection conn = DriverManager.getConnection(DB_URL)) {

            Statement statement = conn.createStatement();
            ResultSet results = statement.executeQuery("select student_id, SUM(points_scored) from PlayGame where student_id = " + student_id);

            
            int id = results.getInt(1);
            total = results.getInt(2);

            PreparedStatement statement2 = conn.prepareStatement("UPDATE Student SET total_xp = " + total + " WHERE id = " + id);
            statement2.execute();
    

        } catch (SQLException ex) {
            ex.printStackTrace();
        }
        return total;
    }

    public static void main(String... args){
        Access ac = new Access();
        // ac.addStudent("Kim", "J", 2020, 11, 31, 4);
        ac.updateXP(1);
    }
}