package me.blahberi.spacelab;

import org.bukkit.Bukkit;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.plugin.java.JavaPlugin;

import org.bukkit.ChatColor;

import org.jetbrains.annotations.NotNull;

import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.Socket;



public final class SpaceLab extends JavaPlugin {
    private String color(String s) { return ChatColor.translateAlternateColorCodes('&', s); }
    Socket rotatorSocket = null;
    Socket arduinoSocket = null;

    String arduinoIP = "127.0.0.1";
    String rotatorIP = "127.0.0.1";

    int arduinoPort = 1234;
    int rotatorPort = 12345;


    @Override
    public void onEnable() {
        this.saveDefaultConfig();
    }

    @Override
    public void onDisable() {
        this.saveConfig();
    }

    @Override
    public boolean onCommand(@NotNull CommandSender sender, @NotNull Command command, @NotNull String label, @NotNull String[] args) {
        if (command.getName().equalsIgnoreCase("connect_rotator")){
            connect_rotator();
        }

        else if(command.getName().equalsIgnoreCase("connect_arduino")){
            connect_arduino();
        }

        else if(command.getName().equalsIgnoreCase("connect_all")){
            connect_rotator();
            connect_arduino();
        }

        else if(command.getName().equalsIgnoreCase("spacelab")){
            OutputStream output = null;
            try {
                output = arduinoSocket.getOutputStream();
                PrintWriter writer = new PrintWriter(output, true);
                writer.write("gaming!<EOF>");
                writer.flush();
            } catch (IOException e) {
                e.printStackTrace();
                return false;
            }
            Bukkit.broadcastMessage(color("&asuccessfully sent motor activation message"));
        }

        else if(command.getName().equalsIgnoreCase("rotate")){
            if (args.length < 1){
                return false;
            }
            OutputStream output = null;
            try {
                output = rotatorSocket.getOutputStream();
                PrintWriter writer = new PrintWriter(output, true);
                writer.write("rotate " + args[0]);
                writer.flush();
            } catch (IOException e) {
                e.printStackTrace();
                return false;
            }
            Bukkit.broadcastMessage(color("&asuccessfully sent rotation activation message"));
        }

        else if (command.getName().equalsIgnoreCase("update_config")){
            updateConfig();
            updateSocketAddress();
            Bukkit.broadcastMessage(color("&aupdated config"));
        }
        return true;
    }

    private void connect_rotator(){
        try {
            rotatorSocket = new Socket(rotatorIP, rotatorPort);
        } catch (IOException e) {
            e.printStackTrace();
            return;
        }
        Bukkit.broadcastMessage(color("&aconnected to rotator"));

    }
    private void connect_arduino(){
        try {
            arduinoSocket = new Socket(arduinoIP, arduinoPort);
        } catch (IOException e){
            e.printStackTrace();
            return;
        }
        Bukkit.broadcastMessage(color("&auconnected to arduino"));

    }

    private void updateConfig(){
        this.reloadConfig();
        this.saveConfig();

    }
    private void updateSocketAddress(){
        arduinoIP = this.getConfig().getString("arduino_ip");
        arduinoPort = this.getConfig().getInt("arduino_port");

        rotatorIP = this.getConfig().getString("rotator_ip");
        rotatorPort = this.getConfig().getInt("rotator_port");
    }
}
