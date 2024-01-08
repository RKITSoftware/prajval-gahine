public class file{
    String name;
    file(String name){
        this.name = name;
    }
    public static void main(String[] args){
        file2 o1 = new file2("prajval", "gahine");
        o1.getName();
    }
}
class file2 extends file{
    String name;
    file2(String name1, String name2){
        super(name1);
        this.name = name2;
    }
    public void getName(){
        System.out.println(super.name);
        System.out.println(this.name);
    }
}
